import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { config } from "../config-development";

export function RequestSummary() {
  const { isAuthenticated } = useAuth0();
  const navigate = useNavigate();
  const location = useLocation();
  const [offer, setOffer] = useState();
  const [requestData, setRequestData] = useState();
  /* TODO Add SummaryData */
  // const [summaryData, setSummaryData] = useState({
  //   userAuth0: "NOT IMPLEMENTED",
  //   user: {
  //     firstName: "Adam",
  //     lastName: "Nowak",
  //     email: "adam.nowak@gmail.com",
  //     companyName: "DruteX",
  //   },
  //   userAddress: {
  //     street: "Radomska 17",
  //     city: "Radom",
  //     postalCode: "000-01",
  //     country: "Polska",
  //   },
  // });
  const [userData, setUserData] = useState({
    auth0Id: "TOKEN",
    inquiryId: "string",
    companyName: "DruteX",
    courierCompany: "XXX",
    firstName: "Adam",
    lastName: "Nowak",
    email: "adam.nowak@gmail.com",

    address: {
      houseNumber: "1",
      apartmentNumber: "",
      street: "Radomska",
      city: "Radom",
      zipCode: "00-001",
      country: "Polska",
    },
  });

  useEffect(() => {
    /* If user logged fill userData */

    setOffer(location.state.selectedOffer);

    setUserData({
      ...userData,
      courierCompany: location.state.selectedOffer.companyName,
      inquiryId: location.state.selectedOffer.inquiryId,
    });

    setRequestData(location.state.requestData);

    // setSummaryData({ ...userData, ...location.state.requestData });
  }, [location.state.selectedOffer, location.state.requestData, userData]);

  const handleChange = (e, key = null) => {
    var newUserData = userData;

    if (key === null) {
      newUserData = {
        ...userData,
        [e.target.name]: e.target.value,
      };
    } else {
      newUserData = {
        ...userData,
        [key]: {
          ...userData[key],
          [e.target.name]: e.target.value,
        },
      };
    }

    setUserData(newUserData);
    // setSummaryData((prev) => ({ ...newUserData, ...prev }));
    // console.log("SUMMARY", summaryData.address.street);
  };

  function UserInformations() {
    return (
      <div>
        <h1>
          Consider log in or <br />
          an account creation
        </h1>

        <label>
          first name:
          <input
            type="text"
            name="firstName"
            value={userData.firstName}
            onChange={handleChange}
          />
        </label>
        <br />
        <label>
          last name:
          <input
            type="text"
            name="lastName"
            value={userData.lastName}
            onChange={handleChange}
          />
        </label>
        <br />
        <label>
          company name:
          <input
            type="text"
            name="companyName"
            value={userData.companyName}
            onChange={handleChange}
          />
        </label>
        <br />
        <label>
          e-mail:
          <input
            type="email"
            name="email"
            value={userData.email}
            onChange={handleChange}
          />
        </label>

        <p>address</p>
        <label>
          street:
          <input
            type="text"
            name="street"
            value={userData.address.street}
            onChange={(e) => handleChange(e, "address")}
          />
        </label>
        <br />
        <label>
          house number:
          <input
            type="text"
            name="houseNumber"
            value={userData.address.houseNumber}
            onChange={(e) => handleChange(e, "address")}
          />
        </label>
        <br />
        <label>
          apartment number:
          <input
            type="text"
            name="apartmentNumber"
            value={userData.address.apartmentNumber}
            onChange={(e) => handleChange(e, "address")}
          />
        </label>
        <br />
        <label>
          city:
          <input
            type="text"
            name="city"
            value={userData.address.city}
            onChange={(e) => handleChange(e, "address")}
          />
        </label>
        <br />
        <label>
          zip code:
          <input
            type="text"
            name="zipCode"
            value={userData.address.zipCode}
            onChange={(e) => handleChange(e, "address")}
          />
        </label>
        <br />
        <label>
          country:
          <input
            type="text"
            name="country"
            value={userData.address.country}
            onChange={(e) => handleChange(e, "address")}
          />
        </label>
      </div>
    );
  }

  const handleClick = async () => {
    try {
      const response = await axios.post(
        `${config.serverUri}/accept-offer`,
        userData
      );

      navigate("/delivery-request/id", {
        state: { requestId: response.data },
      });
    } catch (error) {
      console.error(error);
    }
  };

  function offerDetails() {
    return (
      <div>
        <p>Company: {offer?.companyName}</p>
        <p>Total price: {offer?.totalPrice} PLN</p>{" "}
        {/* TODO Add totalPrice currency */}
        <ul>
          {offer?.priceBreakDown.map((price, index) => (
            <li key={index}>
              {price.description}: {price.amount} {price.currency}
            </li>
          ))}
        </ul>
      </div>
    );
  }

  function showSummary() {
    return <div className="overflow">{renderObjectValues(requestData)}</div>;
  }

  return (
    <>
      <h1>Summary</h1>
      <div className="columns">
        {!isAuthenticated && UserInformations()}
        {showSummary()}
        <div className="overflow">{renderObjectValues(userData)}</div>
        {offerDetails()}
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
