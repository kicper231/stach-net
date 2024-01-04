import { useState } from "react";
import { useNavigate } from "react-router-dom";

const serverUrl = process.env.SERVER_URL;

export function RequestInquiry() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
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

  const handleSubmit = async (e) => {
    e.preventDefault();

    navigate("/delivery-request/offers");

    try {
      // Send the form data to the backend
      const response = await fetch(`${serverUrl}/delivery-request/inquiry`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      // Check if the request was successful (status code 2xx)
      if (response.ok) {
        console.log("Form submitted successfully!");
      } else {
        console.error("Form submission failed:", response.statusText);
      }
    } catch (error) {
      console.error("Error sending form data:", error);
    }
  };

  return (
    <div className="overflow">
      <form onSubmit={handleSubmit}>
        <p>Package</p>

        <label>
          Dimensions:
          <input
            type="text"
            name="dimensions"
            value={formData.package.dimensions}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>
        <br />
        <label>
          Weight (kg):
          <input
            type="number"
            name="weight"
            value={formData.package.weight}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>
        <br />
        <label>
          Priority:
          <br />
          <label>
            <input
              type="radio"
              name="priority"
              value="option1"
              checked={formData.package.priority === true}
              onChange={(e) => handleChange(e, "package")}
              required
            />
            High
          </label>
          <label>
            <input
              type="radio"
              name="priority"
              value="option2"
              checked={formData.package.priority === false}
              onChange={(e) => handleChange(e, "package")}
              required
            />
            Low
          </label>
        </label>
        <br />
        <label>
          Delivery at a weekend:
          <input
            type="checkbox"
            name="weekendDelivery"
            value={!formData.package.weekendDelivery}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>

        <p>Source address</p>

        <label>
          Street:
          <input
            type="text"
            name="street"
            value={formData.sourceAddress.street}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          City:
          <input
            type="text"
            name="city"
            value={formData.sourceAddress.city}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          Postal code:
          <input
            type="text"
            name="postalCode"
            value={formData.sourceAddress.postalCode}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
          Country:
          <input
            type="text"
            name="country"
            value={formData.sourceAddress.country}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>

        <p>Destination address</p>

        <label>
          Street:
          <input
            type="text"
            name="street"
            value={formData.destinationAddress.street}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          City:
          <input
            type="text"
            name="city"
            value={formData.destinationAddress.city}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          Postal code:
          <input
            type="text"
            name="postalCode"
            value={formData.destinationAddress.postalCode}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
          Country:
          <input
            type="text"
            name="country"
            value={formData.destinationAddress.country}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>

        <p>Delivery date</p>

        <label>
          Date:
          <input
            type="date"
            name="deliveryDate"
            value={formData.deliveryDate}
            onChange={handleChange}
          />
        </label>
        <br />

        <button type="submit">Send delivery request</button>
      </form>
    </div>
  );
}
