import React, { useEffect, useState } from "react";
import axios from "axios";
import { useLocation, useNavigate } from "react-router-dom";
import { config } from "../config-development";

export function RequestOffers() {
  const navigate = useNavigate();
  const location = useLocation();
  const [offers, setOffers] = useState();
  const [noDataError, setNoDataError] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      const requestData = location.state?.requestData;

      if (!requestData) {
        setNoDataError(true);
        return;
      }

      try {
        const response = await axios.post(
          `${config.serverUri}/api/requestdelivery`,
          requestData
        );
        setOffers(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchData();
  });

  function offersList() {
    return (
      <>
        <h1>Offers</h1>
        <ul>
          {offers.map((offer, index) => (
            <li
              key={index}
              className="offer"
              onClick={() =>
                navigate("/delivery-request/summary", {
                  state: { selectedOffer: offer },
                })
              }
            >
              {offer.companyName} ({offer.price} PLN)
            </li>
          ))}
        </ul>
      </>
    );
  }

  return (
    <>
      {noDataError ? (
        <h1>No request</h1>
      ) : offers ? (
        offersList()
      ) : (
        <h1>Looking for offers...</h1>
      )}
    </>
  );
}
