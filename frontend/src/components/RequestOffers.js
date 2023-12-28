import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

export function RequestOffers() {
  const navigate = useNavigate();
  const [data, setData] = useState({});
  const [offers, setOffers] = useState([]);

  useEffect(() => {
    setData({ id: 1, data: "abc" });
  }, []);

  useEffect(() => {
    // Function to send data to the server
    const sendDataToServer = async () => {
      try {
        const response = await axios.post(
          "http://localhost:5157/delivery_request/offers",
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
    <div>
      <h1>Waiting for offers</h1>
      <button onClick={() => navigate("/delivery_request/summary")}>
        Imaginary offer
      </button>

      <ul>
        {offers.map((offer, index) => (
          <li key={index}>{offer}</li>
        ))}
      </ul>
    </div>
  );
}
