import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { config } from "../config-development";

export function RequestSummary() {
  const { isAuthenticated, user, isLoading } = useAuth0();
  const navigate = useNavigate();
  const location = useLocation();
  const [offer, setOffer] = useState();
  const [requestData, setRequestData] = useState();
  const [userData, setUserData] = useState({
    auth0Id: "TOKEN",
    inquiryId: "NOT_CHANGED",
    companyName: "DruteX",
    courierCompany: "NOT_CHANGED",
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
    const changeUserData = async () => {
      try {
        const response = await axios.get(
          `${config.serverUri}/users/${user?.sub}`
        );

        setUserData((data) => ({
          ...data,
          auth0Id: user?.sub,
          firstName: response.data.firstName,
          lastName: response.data.lastName,
          email: response.data.email,
        }));
      } catch (error) {
        console.log(error);
      }
    };

    if (isAuthenticated) {
      changeUserData();
    }

    setOffer(location.state.selectedOffer);

    setUserData((data) => ({
      ...data,
      courierCompany: location.state.selectedOffer.companyName,
      inquiryId: location.state.selectedOffer.inquiryId,
    }));

    setRequestData(location.state.requestData);
  }, [
    location.state.selectedOffer,
    location.state.requestData,
    isLoading,
    isAuthenticated,
    user?.sub,
  ]);

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
  };

  function UserInformations() {
    return (
      <div>
        {!isAuthenticated && (
          <p>
            Consider log in or <br />
            an account creation
          </p>
        )}

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
        <p>
          Total price: {offer?.totalPrice} {offer?.currency}
        </p>
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
    if (!requestData) {
      return <h1>No request data</h1>;
    }

    return (
      <ul>
        <li>
          <strong>package dimensions:</strong>
          <br />
          {requestData.package.width}m x {requestData.package.height}m x{" "}
          {requestData.package.length}m
        </li>
        <li>
          <strong>package weight:</strong> {requestData.package.weight}kg
        </li>

        <li>
          <strong>source address:</strong>
          <br />
          {requestData.sourceAddress.street}{" "}
          {requestData.sourceAddress.houseNumber}
          {requestData.sourceAddress.apartmentNumber &&
            " / " + requestData.sourceAddress.apartmentNumber}
          <br />
          {requestData.sourceAddress.city} {requestData.sourceAddress.zipCode},{" "}
          {requestData.sourceAddress.country}
        </li>

        <li>
          <strong>source address:</strong>
          <br />
          {requestData.destinationAddress.street}{" "}
          {requestData.destinationAddress.houseNumber}
          {requestData.destinationAddress.apartmentNumber &&
            " / " + requestData.destinationAddress.apartmentNumber}
          <br />
          {requestData.destinationAddress.city}{" "}
          {requestData.destinationAddress.zipCode},{" "}
          {requestData.destinationAddress.country}
        </li>

        <li>
          <strong>delivery date:</strong> {requestData.deliveryDate}
        </li>
        <li>
          <strong>priority:</strong> {requestData.priority ? "yes" : "no"}
        </li>
        <li>
          <strong>weekend delivery:</strong>{" "}
          {requestData.weekendDelivery ? "yes" : "no"}
        </li>
      </ul>
    );
  }

  return (
    <>
      <h1>Summary</h1>
      <div className="columns">
        {UserInformations()}
        {showSummary()}
        {offerDetails()}
        {/* TODO For debbuging uncomment line below */}
        {/* <div className="overflow">{renderObjectValues(userData)}</div> */}
      </div>
      <button onClick={handleClick}>Submit a request</button>
    </>
  );
}

// const renderObjectValues = (obj) => {
//   return (
//     <ul>
//       {Object.entries(obj).map(([key, value]) => (
//         <li key={key}>
//           {typeof value === "object" ? (
//             <>
//               <strong>{key}:</strong>
//               {renderObjectValues(value)}
//             </>
//           ) : value === true ? (
//             <>
//               <strong>{key}:</strong> true
//             </>
//           ) : value === false ? (
//             <>
//               <strong>{key}:</strong> false
//             </>
//           ) : (
//             <>
//               <strong>{key}:</strong> {value}
//             </>
//           )}
//         </li>
//       ))}
//     </ul>
//   );
// };
