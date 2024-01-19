import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Route, Routes } from "react-router-dom";
import { config } from "../config-development";
import axios from "axios";
import { useAuth0 } from "@auth0/auth0-react";

// TODO Remove
const DATA = {
  user: {
    firstName: "string",
    lastName: "string",
    email: "string",
  },
  inquiry: {
    inquiryID: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    package: {
      width: 0,
      height: 0,
      length: 0,
      weight: 0,
    },
    sourceAddress: {
      houseNumber: "string",
      apartmentNumber: "string",
      street: "string",
      city: "string",
      zipCode: "string",
      country: "string",
    },
    destinationAddress: {
      houseNumber: "string",
      apartmentNumber: "string",
      street: "string",
      city: "string",
      zipCode: "string",
      country: "string",
    },
    inquiryDate: "2024-01-19T09:40:57.248Z",
    deliveryDate: "2024-01-19T09:40:57.248Z",
    weekendDelivery: true,
    priority: 0,
  },
  offer: {
    totalPrice: 0,
    currency: "string",
  },
  delivery: {
    deliveryId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    pickupDate: "2024-01-19T09:40:57.248Z",
    deliveryDate: "2024-01-19T09:40:57.248Z",
    deliveryStatus: "no status",
    courier: {
      createdAt: "2024-01-19T09:40:57.248Z",
      userId: 0,
      auth0Id: "string",
      firstName: "string",
      lastName: "string",
      email: "string",
      numberOfLogins: 0,
    },
  },
};

export function WorkerOffers() {
  const navigate = useNavigate();
  const { user, getAccessTokenSilently } = useAuth0();
  const [offers, setOffers] = useState([]);

  useEffect(() => {
    const getOffers = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await axios.get(
          `${config.serverUri}/office-worker/offers`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        setOffers(response.data);
      } catch (error) {
        console.error(error);
        setOffers([DATA, DATA, DATA]); // TODO Remove
      }
    };

    getOffers();
  }, [getAccessTokenSilently]);

  function OffersTable() {
    return (
      <div className="overflow">
        <h1>Offers</h1>

        <ul>
          {offers.map((offer, index) => (
            <li
              key={index}
              className="offer"
              onClick={() => navigate(`${index}`)}
            >
              <strong>id:</strong> {offer.delivery.deliveryId}
              <br />
              <strong>date:</strong> {offer.delivery.deliveryDate}
              <br />
              <strong>status:</strong> {offer.delivery.deliveryStatus}
            </li>
          ))}
        </ul>
      </div>
    );
  }

  const handleChangeStatus = async (id, status) => {
    try {
      const token = await getAccessTokenSilently();
      await axios.post(
        `${config.serverUri}/office-worker/change-offert-status`,
        {
          deliveryId: id,
          deliveryStatus: status,
          auth0Id: user.sub,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      navigate("/offers"); // TODO Check if works
      window.location.reload();
    } catch (error) {
      console.error(error);
    }
  };

  function Offer({ offer }) {
    return (
      <>
        <div className="contexHolder">
          <h1>Offer</h1>
          <ul>
            <li>
              <strong>id:</strong> {offer.delivery.deliveryId}
            </li>
            <li>
              <strong>cost:</strong> {offer.offer.totalPrice}{" "}
              {offer.offer.currency}
            </li>
            <li>
              <strong>user:</strong> {offer.user.firstName}{" "}
              {offer.user.lastName}, {offer.user.email}
            </li>
            <li>
              <strong>package dimensions:</strong> {offer.inquiry.package.width}
              m x {offer.inquiry.package.height}m x{" "}
              {offer.inquiry.package.length}m
            </li>
            <li>
              <strong>package weight:</strong> {offer.inquiry.package.weight}kg
            </li>
            <li>
              <strong>source address:</strong>
              <br />
              {displayAddress(offer.inquiry.sourceAddress)}
            </li>
            <li>
              <strong>destination address:</strong>
              <br />
              {displayAddress(offer.inquiry.destinationAddress)}
            </li>
            <li>
              <strong>delivery date:</strong> {offer.inquiry.deliveryDate}
            </li>
            <li>
              <strong>priority:</strong> {offer.inquiry.priority ? "yes" : "no"}
            </li>
            <li>
              <strong>weekend delivery:</strong>{" "}
              {offer.inquiry.weekendDelivery ? "yes" : "no"}
            </li>
            <li>
              <strong>status:</strong> {offer.delivery.deliveryStatus}
            </li>
          </ul>
        </div>

        {offer.delivery.deliveryStatus === "no status" && (
          <>
            <button
              onClick={() =>
                handleChangeStatus(offer.delivery.deliveryId, "Accept")
              }
            >
              Accept
            </button>
            <button
              onClick={() =>
                handleChangeStatus(offer.delivery.deliveryId, "Reject")
              }
            >
              Reject
            </button>
          </>
        )}
      </>
    );
  }

  return (
    <>
      <Routes>
        <Route path="/" element={<OffersTable />} />

        {offers.map((offer, index) => (
          <Route
            key={index}
            path={`${index}`}
            element={<Offer offer={offer} />}
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
