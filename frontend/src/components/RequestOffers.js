import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const serverUrl = process.env.SERVER_URL;

export function RequestOffers() {
  const navigate = useNavigate();
  const [data, setData] = useState({});
  const [offers, setOffers] = useState([
    { companyName: "CurrierHub", price: 0.99 },
    { companyName: "Fast curier", price: 13.4 },
    { companyName: "Slow curier", price: 20 },
  ]);

  useEffect(() => {
    setData({ id: 1, data: "abc" });
  }, []);

  useEffect(() => {
    // Function to send data to the server
    const sendDataToServer = async () => {
      try {
        const response = await axios.post(
          `${serverUrl}/delivery-request/offers`,
          data
        );
        console.log("Data sent successfully:", response.data);
      } catch (error) {
        console.error("Error sending data to the server:", error);
      }
    };

    // Function to connect to WebSocket and listen for offers
    const connectToWebSocket = () => {
      const socket = new WebSocket("ws://your-server-websocket-endpoint");

      socket.addEventListener("message", (event) => {
        const newOffer = JSON.parse(event.data);
        setOffers((prevOffers) => [...prevOffers, newOffer]);
      });

      // Close the WebSocket connection after 30 seconds
      setTimeout(() => {
        socket.close();
      }, 30000);
    };

    sendDataToServer();
    connectToWebSocket();
  }, [data]);

  return (
    <>
      <h1>Waiting for offers</h1>

      <ul>
        {offers.map((offer, index) => (
          <li
            key={index}
            className="offer"
            onClick={() => navigate("/delivery-request/summary")}
          >
            {offer.companyName} ({offer.price} PLN)
          </li>
        ))}
      </ul>
    </>
  );
}
