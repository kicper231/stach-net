import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";

export function RequestOffers() {
  const navigate = useNavigate();
  const location = useLocation();
  const [noDataError, setNoDataError] = useState(false);
  const [offers, setOffers] = useState(location.state.offers);
  const [requestData, setRequestData] = useState();

  useEffect(() => {
    setRequestData(location.state.requestData);
    if (!location.state.requestData) {
      setNoDataError(true);
      return;
    }
    setOffers(location.state.offers);
  }, [location.state.offers, location.state.requestData]);

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
                  state: { selectedOffer: offer, requestData: requestData },
                })
              }
            >
              {offer.companyName} ({offer.totalPrice} {offer.currency})
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
