import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate, useLocation as useRouteLocation } from "react-router-dom";

const serverUrl = process.env.REACT_APP_SERVER_URL;

export function RequestOffers() {
  const navigate = useNavigate();
  const location = useRouteLocation();
  const [offers, setOffers] = useState([
    { companyName: "CurrierHub", price: 0.99 },
    { companyName: "Fast curier", price: 13.4 },
    { companyName: "Slow curier", price: 20 },
  ]);  // Bazowe oferty
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      setError(null);

     
      const formData = location.state?.formData;

      if (!formData) {
        setError("Brak danych formularza.");
        setIsLoading(false);
        return;
      }

      try {
        const response = await axios.post(`https://localhost:7161/api/requestdelivery`, formData, {
          timeout: 30000, // 30 sekund timeout
        });

        if (response.status === 200) {
          setOffers(response.data || []); // Ustaw nowe oferty
        } else {
          console.error("Nie udało się pobrać ofert:", response.statusText);
          setError("Nie udało się pobrać ofert.");
        }
      } catch (error) {
        if (error.code === 'ECONNABORTED') {
          console.error("Timeout:", error.message);
          setError("Przekroczono czas oczekiwania na odpowiedź.");
        } else {
          console.error("Wystąpił błąd:", error);
          setError("Wystąpił błąd podczas pobierania ofert.");
        }
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, []);  

  return (
    <>
      <h1>{isLoading ? "Czekamy na oferty..." : "Oferty"}</h1>
      {error && <p>{error}</p>}
      <ul>
        {offers.map((offer, index) => (
          <li
            key={index}
            className="offer"
            onClick={() => navigate("/delivery-request/summary", { state: { selectedOffer: offer } })}
          >
            {offer.companyName} ({offer.price} PLN)
          </li>
        ))}
      </ul>
    </>
  );
        }
