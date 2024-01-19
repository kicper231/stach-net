import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Route, Routes } from "react-router-dom";
import { config } from "../config-development";
import axios from "axios";
import { useAuth0 } from "@auth0/auth0-react";

export function CourierMyDeliveries() {
  const navigate = useNavigate();
  const { user, getAccessTokenSilently } = useAuth0();
  const [myDeliveries, setMyDeliveries] = useState([]);

  useEffect(() => {
    const getMyDeliveries = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await axios.get(
          `${config.serverUri}/courier/get-all-my-delivery/${user.sub}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        setMyDeliveries(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    getMyDeliveries();
  }, [user.sub, getAccessTokenSilently]);

  function DeliveriesTable() {
    const [showAccepted, setShowAccepted] = useState(true);
    const [showPickedUp, setShowPickedUp] = useState(true);
    const [showDelivered, setShowDelivered] = useState(true);
    const [showCannotDeliver, setShowCannotDeliver] = useState(true);

    const handleChangeAccepted = () => {
      setShowAccepted(!showAccepted);
    };

    const handleChangePickedUp = () => {
      setShowPickedUp(!showPickedUp);
    };

    const handleChangeDelivered = () => {
      setShowDelivered(!showDelivered);
    };

    const handleChangeCannotDeliver = () => {
      setShowCannotDeliver(!showCannotDeliver);
    };

    const list = [];

    if (!myDeliveries) {
      return <p>Loading...</p>;
    }

    myDeliveries.forEach((delivery, index) => {
      if (
        delivery.delivery.deliveryStatus === "accepted by courier" &&
        !showAccepted
      ) {
        return;
      } else if (
        delivery.delivery.deliveryStatus === "picked up" &&
        !showPickedUp
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
            checked={showPickedUp}
            onChange={handleChangePickedUp}
          />
          picked up
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

      navigate("/my-deliveries");
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

        {delivery.delivery.deliveryStatus === "accepted by courier" && (
          <button
            onClick={() =>
              handleChangeStatus(
                delivery.delivery.deliveryId,
                "picked up",
                "message"
              )
            }
          >
            Picked up
          </button>
        )}

        {delivery.delivery.deliveryStatus === "picked up" && (
          <>
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

        {myDeliveries.map((delivery, index) => (
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
