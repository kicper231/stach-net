import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Route, Routes } from "react-router-dom";
import { config } from "../config-development";
import axios from "axios";
import { useAuth0 } from "@auth0/auth0-react";
import { SearchBar } from "./SearchBar";

// TODO Remove
const DATA = {
  inquiryId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  user: {
    firstName: "string",
    lastName: "string",
    email: "string",
  },
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
  inquiryDate: "2024-01-19T08:43:03.235Z",
  weekendDelivery: true,
  priority: 0,
};

export function WorkerInquiries() {
  const navigate = useNavigate();
  const { getAccessTokenSilently } = useAuth0();
  const [inquiries, setInquiries] = useState([]);

  useEffect(() => {
    const getInquirues = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await axios.get(
          `${config.serverUri}/office-worker/inquiries`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        setInquiries(response.data);
      } catch (error) {
        console.error(error);
        setInquiries([DATA, DATA, DATA]); // TODO Remove
      }
    };

    getInquirues();
  }, [getAccessTokenSilently]);

  function InquiriesTable() {
    const [filterText, setFilterText] = useState("");
    const list = [];

    if (!inquiries) {
      return <p>Loading...</p>;
    }

    inquiries.forEach((inquiry, index) => {
      if (
        inquiry.inquiryId.toLowerCase().indexOf(filterText.toLowerCase()) === -1
      ) {
        return;
      }

      list.push(
        <li
          key={index}
          className="inquiry"
          onClick={() => navigate(`${index}`)}
        >
          <strong>id:</strong> {inquiry.inquiryId}
          <br />
          <strong>date:</strong> {inquiry.inquiryDate}
        </li>
      );
    });

    return (
      <div className="overflow">
        <h1>All inquiries</h1>
        <SearchBar filterText={filterText} onFilterTextChange={setFilterText} />
        <ul>{list}</ul>
      </div>
    );
  }

  return (
    <>
      <Routes>
        <Route path="/" element={<InquiriesTable />} />

        {inquiries.map((inquiry, index) => (
          <Route
            key={index}
            path={`${index}`}
            element={<Inquiry inquiry={inquiry} />}
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

function Inquiry({ inquiry }) {
  return (
    <>
      <div className="contexHolder">
        <h1>Inquiry</h1>
        <ul>
          <li>
            <strong>id:</strong> {inquiry.inquiryId}
          </li>
          <li>
            <strong>user:</strong> {inquiry.user.firstName}{" "}
            {inquiry.user.lastName}, {inquiry.user.email}
          </li>
          <li>
            <strong>package dimensions:</strong> {inquiry.package.width}m x{" "}
            {inquiry.package.height}m x {inquiry.package.length}m
          </li>
          <li>
            <strong>package weight:</strong> {inquiry.package.weight}kg
          </li>

          <li>
            <strong>source address:</strong>
            <br />
            {displayAddress(inquiry.sourceAddress)}
          </li>

          <li>
            <strong>destination address:</strong>
            <br />
            {displayAddress(inquiry.destinationAddress)}
          </li>

          <li>
            <strong>delivery date:</strong> {inquiry.deliveryDate}
          </li>
          <li>
            <strong>priority:</strong> {inquiry.priority ? "yes" : "no"}
          </li>
          <li>
            <strong>weekend delivery:</strong>{" "}
            {inquiry.weekendDelivery ? "yes" : "no"}
          </li>
        </ul>
      </div>
    </>
  );
}
