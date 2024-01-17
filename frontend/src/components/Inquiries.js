import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Route, Routes } from "react-router-dom";
import { config } from "../config-development";
import axios from "axios";
import { useAuth0 } from "@auth0/auth0-react";

export function Inquiries() {
  const navigate = useNavigate();
  const { user, getAccessTokenSilently } = useAuth0();
  const [inquiries, setInquiries] = useState([]);

  useEffect(() => {
    const getInquirues = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await axios.get(
          `${config.serverUri}/get-my-inquiries/${user.sub}`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        setInquiries(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    getInquirues();
  }, [user.sub, getAccessTokenSilently]);

  function InquiriesTable() {
    const [addedInquiryId, setAddedInquiryId] = useState("");
    const [isError, setIsError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("Error");

    const handleAdd = async () => {
      try {
        await axios.post(`${config.serverUri}/add-delivery`, {
          deliveryID: addedInquiryId,
          userAuth0: user.sub,
        });

        window.location.reload();
      } catch (error) {
        setIsError(true);
        typeof error.response.data === "string" &&
          setErrorMessage(error.response.data);
        console.error(error);
      }
    };

    return (
      <div className="overflow">
        <h1>Inquiries</h1>

        <ul>
          {inquiries.map((inquiry, index) => (
            <li
              key={index}
              className="inquiry"
              onClick={() => navigate(`${index}`)}
            >
              <strong>date:</strong> {inquiry.deliveryDate} /{" "}
              <strong>source address:</strong> {inquiry.sourceAddress.street},{" "}
              {inquiry.sourceAddress.city} /{" "}
              <strong>destination address:</strong>{" "}
              {inquiry.destinationAddress.street},{" "}
              {inquiry.destinationAddress.city}
            </li>
          ))}
        </ul>

        <button onClick={handleAdd}>Add inquiry</button>
        <br />
        <input
          type="text"
          value={addedInquiryId}
          placeholder="added inquiry id"
          onChange={(e) => setAddedInquiryId(e.target.value)}
        />
        <br />
        {isError && <h1 className="red">{errorMessage}</h1>}
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

function Inquiry({ inquiry }) {
  return (
    <>
      <div className="contexHolder">
        <h1>Inquiry</h1>
        <ul>
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
            {inquiry.sourceAddress.street} {inquiry.sourceAddress.houseNumber}
            {inquiry.sourceAddress.apartmentNumber &&
              " / " + inquiry.sourceAddress.apartmentNumber}
            <br />
            {inquiry.sourceAddress.city} {inquiry.sourceAddress.zipCode},{" "}
            {inquiry.sourceAddress.country}
          </li>

          <li>
            <strong>source address:</strong>
            <br />
            {inquiry.destinationAddress.street}{" "}
            {inquiry.destinationAddress.houseNumber}
            {inquiry.destinationAddress.apartmentNumber &&
              " / " + inquiry.destinationAddress.apartmentNumber}
            <br />
            {inquiry.destinationAddress.city}{" "}
            {inquiry.destinationAddress.zipCode},{" "}
            {inquiry.destinationAddress.country}
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
