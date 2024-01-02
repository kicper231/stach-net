import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Route, Routes, useRoutes } from "react-router-dom";

const serverUrl = process.env.SERVER_URL;

const REQUEST = {
  id: 5823572835,
  package: {
    dimensions: "0.2m x 0.3m x 0.4m",
    weight: 10,
    priority: false,
    weekendDelivery: false,
  },
  sourceAddress: {
    street: "Lubelska 13",
    city: "Lublin",
    postalCode: "20-000",
    country: "Polska",
  },
  destinationAddress: {
    street: "Nowowiejska 2",
    city: "Warszawa",
    postalCode: "000-00",
    country: "Polska",
  },
  deliveryDate: "2024-2-27",
};

export function Requests() {
  const navigate = useNavigate();
  const [requests, setRequests] = useState([REQUEST, REQUEST, REQUEST]);

  useEffect(() => {
    fetch(`${serverUrl}/requests`, { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        setRequests(data);
      })
      .catch((error) => console.error(error));
  });

  const routes = useRoutes(
    requests.map((request) => {
      return {
        path: `${request.id}`,
        element: <Request request={request} />,
      };
    })
  );

  function RequestsTable() {
    return (
      <>
        <h1>Requests</h1>

        <ul>
          {requests.map((request, index) => (
            <li
              key={index}
              className="request"
              onClick={() => navigate(`${request.id}`)}
            >
              id: {request.id} / Source address: {request.sourceAddress.street}{" "}
              / Destination address: {request.destinationAddress.street}
            </li>
          ))}
        </ul>
      </>
    );
  }

  return (
    <>
      <Routes>
        <Route path="/" element={<RequestsTable />} />
      </Routes>
      {routes}
    </>
  );
}

function Request({ request }) {
  return (
    <>
      <div className="contexHolder">
        <h1>Request</h1>
        <p>Id: {request.id}</p>
        <p>Delivery date: {request.deliveryDate}</p>
        <p>
          Source address: {request.sourceAddress.street},{" "}
          {request.sourceAddress.city} {request.sourceAddress.postalCode},{" "}
          {request.sourceAddress.country}
        </p>
        <p>
          Destination address: {request.destinationAddress.street},{" "}
          {request.destinationAddress.city}{" "}
          {request.destinationAddress.postalCode},{" "}
          {request.destinationAddress.country}
        </p>
      </div>
    </>
  );
}
