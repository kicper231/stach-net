import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useState, useEffect } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { useLocation } from "react-router-dom";
const serverUrl = process.env.SERVER_URL;

const summaryData = {
  PriceSplitOnTaxes: 12,
  ServiceFee: 10,
  DistanceFee: 2,
  AdditionalFees: 0,
};

export function RequestSummary() {

  
  const { isAuthenticated } = useAuth0();
  const navigate = useNavigate();
  const location = useLocation();
  const selectedOffer = location.state?.selectedOffer;
  console.log(selectedOffer);
  const [imaginaryData, setImaginaryData] = useState({
    userAuth0: "NOT IMPLEMENTED",
    user: {
      firstName: "Adam",
      lastName: "Nowak",
      email: "adam.nowak@gmail.com",
      companyName: "DruteX",
    },
    userAddress: {
      street: "Radomska 17",
      city: "Radom",
      postalCode: "000-01",
      country: "Polska",
    },
    selectedOfferCompany:  selectedOffer.companyName,
    selectedOfferId: selectedOffer.inquiryId,
  });
  const handleChange = (e, key = null) => {
    var value = e.target.value;

    if (key === null) {
      setImaginaryData({
        ...imaginaryData,
        [e.target.name]: value,
      });
    } else {
      setImaginaryData({
        ...imaginaryData,
        [key]: {
          ...imaginaryData[key],
          [e.target.name]: value,
        },
      });
    }
  };

  

  function UserInformations() {
    return (
      <div>
        <h1>
          Consider log in or <br />
          an account creation
        </h1>
        <label>
          Name:
          <input
            type="text"
            name="firstName"
            value={imaginaryData.user.firstName}
            onChange={(e) => handleChange(e, "user")}
          />
        </label>
        <br />
        <label>
          Nazwisko:
          <input
            type="text"
            name="lastName"
            value={imaginaryData.user.lastName}
            onChange={(e) => handleChange(e, "user")}
          />
        </label>
        <br />
        <label>
          Ulica:
          <input
            type="text"
            name="street"
            value={imaginaryData.userAddress.street}
            onChange={(e) => handleChange(e, "userAddress")}
          />
        </label>
        <br />
        <label>
          Miasto:
          <input
            type="text"
            name="city"
            value={imaginaryData.userAddress.city}
            onChange={(e) => handleChange(e, "userAddress")}
          />
        </label>
        <br />
        <label>
          Kod pocztowy:
          <input
            type="text"
            name="postalCode"
            value={imaginaryData.userAddress.postalCode}
            onChange={(e) => handleChange(e, "userAddress")}
          />
        </label>
        <br />
        <label>
          Kraj:
          <input
            type="text"
            name="country"
            value={imaginaryData.userAddress.country}
            onChange={(e) => handleChange(e, "userAddress")}
          />
        </label>
        <br />
        <label>
          E-mail:
          <input
            type="email"
            name="email"
            value={imaginaryData.user.email}
            onChange={(e) => handleChange(e, "user")}
          />
        </label>
        <br />
        <label>
          Nazwa firmy:
          <input
            type="text"
            name="companyName"
            value={imaginaryData.user.companyName}
            onChange={(e) => handleChange(e, "user")}
          />
        </label>
      </div>
    );
  }

  function handleClick() {
    const sendDataToServer = async () => {
      try {
        const response = await axios.post(
          `${serverUrl}/delivery-request/submit`,
          imaginaryData
        );
        console.log("Data sent successfully:", response.data);
      } catch (error) {
        console.error("Error sending data to the server:", error);
      }
    };

    sendDataToServer();
    navigate("/delivery-request/id");
  }

  return (
    <>
      <h1>Summary</h1>
      <div className="columns">
        {!isAuthenticated && UserInformations()}
        <div className="overflow">{renderObjectValues(imaginaryData)}</div>
        {selectedOffer && renderObjectValues(selectedOffer)} {/* Wyświetlanie szczegółów wybranej oferty */}
      </div>
      <button onClick={handleClick}>Submit a request</button>
    </>
  );

}

const renderObjectValues = (obj) => {
  return (
    <ul>
      {Object.entries(obj).map(([key, value]) => (
        <li key={key}>
          {typeof value === "object" ? (
            <>
              <strong>{key}:</strong>
              {renderObjectValues(value)}
            </>
          ) : value === true ? (
            <>
              <strong>{key}:</strong> true
            </>
          ) : value === false ? (
            <>
              <strong>{key}:</strong> false
            </>
          ) : (
            <>
              <strong>{key}:</strong> {value}
            </>
          )}
        </li>
      ))}
    </ul>
  );
};
