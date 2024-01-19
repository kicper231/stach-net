import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Route, Routes } from "react-router-dom";
import { config } from "../config-development";
import axios from "axios";
import { useAuth0 } from "@auth0/auth0-react";

export function CourierDeliveries() {
  const navigate = useNavigate();
  const { user, getAccessTokenSilently } = useAuth0();
  const [deliveries, setDeliveries] = useState([]);

  useEffect(() => {
    const getDeliveries = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await axios.get(
          `${config.serverUri}/courier/get-all-available-delivery`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        setDeliveries(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    getDeliveries();
  }, [getAccessTokenSilently]);

  function DeliveriesTable() {
    const [showAccepted, setShowAccepted] = useState(true);
    const [showReceived, setShowReceived] = useState(true);
    const [showDelivered, setShowDelivered] = useState(true);
    const [showCannotDeliver, setShowCannotDeliver] = useState(true);

    const handleChangeAccepted = () => {
      setShowAccepted(!showAccepted);
    };

    const handleChangeReceived = () => {
      setShowReceived(!showReceived);
    };

    const handleChangeDelivered = () => {
      setShowDelivered(!showDelivered);
    };

    const handleChangeCannotDeliver = () => {
      setShowCannotDeliver(!showCannotDeliver);
    };

    const list = [];

    if (!deliveries) {
      return <p>Loading...</p>;
    }

    deliveries.forEach((delivery, index) => {
      if (delivery.delivery.deliveryStatus === "accepted" && !showAccepted) {
        return;
      } else if (
        delivery.delivery.deliveryStatus === "received" &&
        !showReceived
      ) {
        return;
      } else if (
        delivery.delivery.deliveryStatus === "delivered" &&
        !showDelivered
      ) {
        return;
      } else if (
        delivery.delivery.deliveryStatus === "cannot deliver" &&
        !showCannotDeliver
      ) {
        return;
      }

      list.push(
        <li
          key={index}
          className="delivery"
          onClick={() => navigate(`${index}`)}
        >
          <strong>date:</strong> {delivery.delivery.deliveryDate} /{" "}
          <strong>source address:</strong>{" "}
          {delivery.inquiry.sourceAddress.street},{" "}
          {delivery.inquiry.sourceAddress.city} /{" "}
          <strong>destination address:</strong>{" "}
          {delivery.inquiry.destinationAddress.street},{" "}
          {delivery.inquiry.destinationAddress.city}, <strong>status:</strong>{" "}
          {delivery.delivery.deliveryStatus}
        </li>
      );
    });

    return (
      <div className="overflow">
        <h1>Deliveries</h1>

        <p>status filtering:</p>
        <label>
          <input
            type="checkbox"
            checked={showAccepted}
            onChange={handleChangeAccepted}
          />
          accepted
        </label>
        <label>
          <input
            type="checkbox"
            checked={showReceived}
            onChange={handleChangeReceived}
          />
          received
        </label>
        <label>
          <input
            type="checkbox"
            checked={showDelivered}
            onChange={handleChangeDelivered}
          />
          delivered
        </label>
        <label>
          <input
            type="checkbox"
            checked={showCannotDeliver}
            onChange={handleChangeCannotDeliver}
          />
          cannot deliver
        </label>

        <ul>{list}</ul>
      </div>
    );
  }

  const handleChangeStatus = async (id, status, message) => {
    try {
      const token = await getAccessTokenSilently();
      await axios.post(
        `${config.serverUri}/courier/change-delivery-status`,
        {
          deliveryId: id,
          deliveryStatus: status,
          message: message,
          auth0Id: user.sub,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      navigate("/deliveries");
      window.location.reload();
    } catch (error) {
      console.error(error);
    }
  };

  function Delivery({ delivery }) {
    return (
      <>
        <div className="contexHolder">
          <h1>Delivery</h1>
          <ul>
            <li>
              <strong>id:</strong> {delivery.delivery.deliveryId}
            </li>
            <li>
              <strong>cost:</strong> {delivery.offer.totalPrice}{" "}
              {delivery.offer.currency}
            </li>
            {delivery.user && (
              <li>
                <strong>user:</strong> {delivery.user.firstName}{" "}
                {delivery.user.lastName}, {delivery.user.email}
              </li>
            )}
            <li>
              <strong>package dimensions:</strong>{" "}
              {delivery.inquiry.package.width}m x{" "}
              {delivery.inquiry.package.height}m x{" "}
              {delivery.inquiry.package.length}m
            </li>
            <li>
              <strong>package weight:</strong> {delivery.inquiry.package.weight}
              kg
            </li>
            <li>
              <strong>source address:</strong>
              <br />
              {displayAddress(delivery.inquiry.sourceAddress)}
            </li>
            <li>
              <strong>destination address:</strong>
              <br />
              {displayAddress(delivery.inquiry.destinationAddress)}
            </li>
            <li>
              <strong>delivery date:</strong> {delivery.inquiry.deliveryDate}
            </li>
            <li>
              <strong>priority:</strong>{" "}
              {delivery.inquiry.priority ? "yes" : "no"}
            </li>
            <li>
              <strong>weekend delivery:</strong>{" "}
              {delivery.inquiry.weekendDelivery ? "yes" : "no"}
            </li>
            <li>
              <strong>status:</strong> {delivery.delivery.deliveryStatus}
            </li>
          </ul>
        </div>

        {delivery.delivery.deliveryStatus !== "no status" && (
          <>
            <button
              onClick={() =>
                handleChangeStatus(
                  delivery.delivery.deliveryId,
                  "received",
                  "message"
                )
              }
            >
              Received
            </button>
            <button
              onClick={() =>
                handleChangeStatus(
                  delivery.delivery.deliveryId,
                  "accepted",
                  "message"
                )
              }
            >
              Cancel
            </button>
            <button
              onClick={() =>
                handleChangeStatus(
                  delivery.delivery.deliveryId,
                  "delivered",
                  "message"
                )
              }
            >
              Delivered
            </button>
            <button
              onClick={() =>
                handleChangeStatus(
                  delivery.delivery.deliveryId,
                  "cannot deliver",
                  "message"
                )
              }
            >
              Cannot deliver
            </button>
          </>
        )}
      </>
    );
  }

  return (
    <>
      <Routes>
        <Route path="/" element={<DeliveriesTable />} />

        {deliveries.map((delivery, index) => (
          <Route
            key={index}
            path={`${index}`}
            element={<Delivery delivery={delivery} />}
          />
        ))}
      </Routes>
    </>
  );
}

function displayAddress(address) {
  return (
    <>
      {address.street} {address.houseNumber}{" "}
      {address.apartmentNumber && " / " + address.apartmentNumber},
      <br />
      {address.city} {address.zipCode}, {address.country}
    </>
  );
}
