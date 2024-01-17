import axios from "axios";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { config } from "../config-development";


export function RequestInquiry() {

  const navigate = useNavigate();
  const [waiting, setWaiting] = useState(false);
  const [formData, setFormData] = useState({
    userAuth0: "TOKEN",
    package: {
      width: 1.5,
      height: 1,
      length: 3,
      weight: 10,
    },
    sourceAddress: {
      houseNumber: "1",
      apartmentNumber: "",
      street: "Lubelska",
      city: "Lublin",
      zipCode: "20-000",
      country: "Polska",
    },
    destinationAddress: {
      houseNumber: "13",
      apartmentNumber: "304",
      street: "Nowowiejska",
      city: "Warszawa",
      zipCode: "000-00",
      country: "Polska",
    },
    deliveryDate: "2024-02-29",
    priority: false,
    weekendDelivery: false,
  });



  const handleChange = (e, key = null) => {
    var value = e.target.value;

    if (e.target.name === "weekendDelivery") {
      value = e.target.checked;
    }

    if (e.target.name === "priority") {
      value = value === "option1" ? true : false;
    }

    if (key === null) {
      setFormData({
        ...formData,
        [e.target.name]: value,
      });
    } else {
      setFormData({
        ...formData,
        [key]: {
          ...formData[key],
          [e.target.name]: value,
        },
      });
    }
  };

  const handleSend = async () => {
    try {
      const promise = axios.post(`${config.serverUri}/send-inquiry`, formData);

      setWaiting(true);

      const response = await promise;

      navigate("/delivery-request/offers", {
        state: { requestData: formData, offers: response.data },
      });
    } catch (error) {
      console.error(error);
    }
  };

  function form() {
    return (
      <>
        <p>Package details:</p>
        <label>
          width (m):
          <input
            type="number"
            name="width"
            value={formData.package.width}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>
        <br />
        <label>
          height (m):
          <input
            type="number"
            name="height"
            value={formData.package.height}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>
        <br />
        <label>
          length (m):
          <input
            type="number"
            name="length"
            value={formData.package.length}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>
        <br />
        <label>
          weight (kg):
          <input
            type="number"
            name="weight"
            value={formData.package.weight}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>

        <p>Source address:</p>
        <label>
          street:
          <input
            type="text"
            name="street"
            value={formData.sourceAddress.street}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          house number:
          <input
            type="text"
            name="houseNumber"
            value={formData.sourceAddress.houseNumber}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          apartment number:
          <input
            type="text"
            name="apartmentNumber"
            value={formData.sourceAddress.apartmentNumber}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          city:
          <input
            type="text"
            name="city"
            value={formData.sourceAddress.city}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          zip code:
          <input
            type="text"
            name="zipCode"
            value={formData.sourceAddress.zipCode}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          country:
          <input
            type="text"
            name="country"
            value={formData.sourceAddress.country}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>

        <p>Destination address:</p>
        <label>
          street:
          <input
            type="text"
            name="street"
            value={formData.destinationAddress.street}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          house number:
          <input
            type="text"
            name="houseNumber"
            value={formData.destinationAddress.houseNumber}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          apartment number:
          <input
            type="text"
            name="apartmentNumber"
            value={formData.destinationAddress.apartmentNumber}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          city:
          <input
            type="text"
            name="city"
            value={formData.destinationAddress.city}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          zip code:
          <input
            type="text"
            name="zipCode"
            value={formData.destinationAddress.zipCode}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          country:
          <input
            type="text"
            name="country"
            value={formData.destinationAddress.country}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>

        <p>Delivery details:</p>
        <label>
          date:
          <input
            type="date"
            name="deliveryDate"
            value={formData.deliveryDate}
            onChange={handleChange}
          />
        </label>
        <br />
        <label>
          priority:
          <label>
            <input
              type="radio"
              name="priority"
              value="option1"
              checked={formData.priority}
              onChange={(e) => handleChange(e)}
              required
            />
            high
          </label>
          <label>
            <input
              type="radio"
              name="priority"
              value="option2"
              checked={!formData.priority}
              onChange={(e) => handleChange(e)}
              required
            />
            low
          </label>
        </label>
        <br />
        <label>
          delivery at a weekend:
          <input
            type="checkbox"
            name="weekendDelivery"
            value={!formData.weekendDelivery}
            onChange={(e) => handleChange(e)}
          />
        </label>
        <br />
      </>
    );
  }

  return (
    <>
      <div className="overflow">{form()}</div>
      <button onClick={handleSend}>Send delivery request</button>
      {waiting && <h1>Looking for offers . . .</h1>}
    </>
  );
}
