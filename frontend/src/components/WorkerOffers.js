import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Route, Routes } from "react-router-dom";
import { config } from "../config-development";
import axios from "axios";
import { useAuth0 } from "@auth0/auth0-react";

export function WorkerOffers() {
  const navigate = useNavigate();
  const { user, getAccessTokenSilently } = useAuth0();
  const [offers, setOffers] = useState([]);

  useEffect(() => {
    const getOffers = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await axios.get(
          `${config.serverUri}/office-worker/get-all-deliveries`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        setOffers(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    getOffers();
  }, [getAccessTokenSilently]);

  function OffersTable() {
    const [sortedOffers, setSortedOffers] = useState(offers);
    const [showAccepted, setShowAccepted] = useState(true);
    const [showRejected, setShowRejected] = useState(true);
    const [showNoStatus, setShowNoStatus] = useState(true);
    const [sortByDate, setSortByDate] = useState(false);

    useEffect(() => {
      if (!sortByDate) {
        setSortedOffers(offers);
      } else {
        setSortedOffers(
          [...offers].sort((a, b) =>
            a.delivery.deliveryDate.localeCompare(b.delivery.deliveryDate)
          )
        );
      }
    }, [sortByDate]);

    const handleChangeNoStatus = () => {
      setShowNoStatus(!showNoStatus);
    };

    const handleChangeAccepted = () => {
      setShowAccepted(!showAccepted);
    };

    const handleChangeRejected = () => {
      setShowRejected(!showRejected);
    };

    const handleChangeSort = () => {
      setSortByDate(!sortByDate);
    };

    const list = [];

    if (!offers) {
      return <p>Loading...</p>;
    }

    sortedOffers.forEach((offer, index) => {
      if (offer.delivery.deliveryStatus === "no status" && !showNoStatus) {
        return;
      } else if (
        offer.delivery.deliveryStatus === "rejected" &&
        !showRejected
      ) {
        return;
      } else if (
        offer.delivery.deliveryStatus !== "no status" &&
        offer.delivery.deliveryStatus !== "rejected" &&
        !showAccepted
      ) {
        return;
      }

      list.push(
        <li key={index} className="offer" onClick={() => navigate(`${index}`)}>
          <strong>id:</strong> {offer.delivery.deliveryId}
          <br />
          <strong>date:</strong> {offer.delivery.deliveryDate}
          <br />
          <strong>status:</strong> {offer.delivery.deliveryStatus}
        </li>
      );
    });

    return (
      <div className="overflow">
        <h1>Offers</h1>

        <p>sorting:</p>
        <label>
          <input
            type="checkbox"
            checked={sortByDate}
            onChange={handleChangeSort}
          />
          sort by date
        </label>

        <p>status filtering:</p>
        <label>
          <input
            type="checkbox"
            checked={showNoStatus}
            onChange={handleChangeNoStatus}
          />
          no status
        </label>
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
            checked={showRejected}
            onChange={handleChangeRejected}
          />
          rejected
        </label>

        <ul>{list}</ul>
      </div>
    );
  }

  const handleChangeStatus = async (id, status) => {
    try {
      const token = await getAccessTokenSilently();
      await axios.post(
        `${config.serverUri}/office-worker/change-delivery-status`,
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

      navigate("/offers");
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
            {offer.user && (
              <li>
                <strong>user:</strong> {offer.user.firstName}{" "}
                {offer.user.lastName}, {offer.user.email}
              </li>
            )}
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
                handleChangeStatus(offer.delivery.deliveryId, "accepted")
              }
            >
              Accept
            </button>
            <button
              onClick={() =>
                handleChangeStatus(offer.delivery.deliveryId, "rejected")
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
