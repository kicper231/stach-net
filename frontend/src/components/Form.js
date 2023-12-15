import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";

export default function Form() {
  const { isAuthenticated, getAccessTokenSilently } = useAuth0();
  const [token, SetToken] = useState();

  useEffect(() => {
    const getToken = async () => {
      try {
        const token = await getAccessTokenSilently();
        SetToken(token);
      } catch (error) {
        console.log("Error");
      }
    };

    getToken();
  });

  console.log(token);

  const [formData, setFormData] = useState({
    userAuth0: token,
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
    package: {
      dimensions: "20cm x 30cm x 40cm",
      weight: 10,
      priority: false,
      weekendDelivery: false,
    },
    sourceAddress: {
      street: "Mozart 3",
      city: "Berlin",
      postalCode: "123-45",
      country: "Niemcy",
    },
    destinationAddress: {
      street: "Nowowiejska 2",
      city: "Warszawa",
      postalCode: "000-00",
      country: "Polska",
    },
    deliveryDate: "2023-12-14",
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

    try {
      // Send the form data to the backend
      const response = await fetch("http://localhost:5157/api/test_fetch", {
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
    <div className="columns">
      <div className="overflow">
        <form onSubmit={handleSubmit}>
          {!isAuthenticated && (
            <>
              <p>Użytkownik niezalogowany</p>
              <label>
                Imię:
                <input
                  type="text"
                  name="firstName"
                  value={formData.user.firstName}
                  onChange={(e) => handleChange(e, "user")}
                />
              </label>
              <br />
              <label>
                Nazwisko:
                <input
                  type="text"
                  name="lastName"
                  value={formData.user.lastName}
                  onChange={(e) => handleChange(e, "user")}
                />
              </label>
              <br />
              <label>
                Ulica:
                <input
                  type="text"
                  name="street"
                  value={formData.userAddress.street}
                  onChange={(e) => handleChange(e, "userAddress")}
                />
              </label>
              <br />
              <label>
                Miasto:
                <input
                  type="text"
                  name="city"
                  value={formData.userAddress.city}
                  onChange={(e) => handleChange(e, "userAddress")}
                />
              </label>
              <br />
              <label>
                Kod pocztowy:
                <input
                  type="text"
                  name="postalCode"
                  value={formData.userAddress.postalCode}
                  onChange={(e) => handleChange(e, "userAddress")}
                />
              </label>
              <br />
              <label>
                Kraj:
                <input
                  type="text"
                  name="country"
                  value={formData.userAddress.country}
                  onChange={(e) => handleChange(e, "userAddress")}
                />
              </label>
              <br />
              <label>
                E-mail:
                <input
                  type="email"
                  name="email"
                  value={formData.user.email}
                  onChange={(e) => handleChange(e, "user")}
                />
              </label>
              <br />
              <label>
                Nazwa firmy:
                <input
                  type="text"
                  name="companyName"
                  value={formData.user.companyName}
                  onChange={(e) => handleChange(e, "user")}
                />
              </label>
            </>
          )}

          <p>Paczka</p>

          <label>
            Wymiary:
            <input
              type="text"
              name="dimensions"
              value={formData.package.dimensions}
              onChange={(e) => handleChange(e, "package")}
            />
          </label>
          <br />
          <label>
            Waga (kg):
            <input
              type="number"
              name="weight"
              value={formData.package.weight}
              onChange={(e) => handleChange(e, "package")}
            />
          </label>
          <br />
          <label>
            Priorytet:
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
              Wysoki
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
              Niski
            </label>
          </label>
          <br />
          <label>
            Dostawa weekendowa:
            <input
              type="checkbox"
              name="weekendDelivery"
              value={!formData.package.weekendDelivery}
              onChange={(e) => handleChange(e, "package")}
            />
          </label>

          <p>Adres nadawcy</p>

          <label>
            Ulica:
            <input
              type="text"
              name="street"
              value={formData.sourceAddress.street}
              onChange={(e) => handleChange(e, "sourceAddress")}
            />
          </label>
          <br />
          <label>
            Miasto:
            <input
              type="text"
              name="city"
              value={formData.sourceAddress.city}
              onChange={(e) => handleChange(e, "sourceAddress")}
            />
          </label>
          <br />
          <label>
            Kod pocztowy:
            <input
              type="text"
              name="postalCode"
              value={formData.sourceAddress.postalCode}
              onChange={(e) => handleChange(e, "sourceAddress")}
            />
          </label>
          <br />
          <label>
            Kraj:
            <input
              type="text"
              name="country"
              value={formData.sourceAddress.country}
              onChange={(e) => handleChange(e, "sourceAddress")}
            />
          </label>

          <p>Adres odbiorcy</p>

          <label>
            Ulica:
            <input
              type="text"
              name="street"
              value={formData.destinationAddress.street}
              onChange={(e) => handleChange(e, "destinationAddress")}
            />
          </label>
          <br />
          <label>
            Miasto:
            <input
              type="text"
              name="city"
              value={formData.destinationAddress.city}
              onChange={(e) => handleChange(e, "destinationAddress")}
            />
          </label>
          <br />
          <label>
            Kod pocztowy:
            <input
              type="text"
              name="postalCode"
              value={formData.destinationAddress.postalCode}
              onChange={(e) => handleChange(e, "destinationAddress")}
            />
          </label>
          <br />
          <label>
            Kraj:
            <input
              type="text"
              name="country"
              value={formData.destinationAddress.country}
              onChange={(e) => handleChange(e, "destinationAddress")}
            />
          </label>

          <p>Data dostarczenia</p>

          <label>
            Data:
            <input
              type="date"
              name="deliveryDate"
              value={formData.deliveryDate}
              onChange={handleChange}
            />
          </label>
          <br />

          <button type="submit">Wyślij zgłoszenie</button>
        </form>
      </div>
      <div className="overflow">{renderObjectValues(formData)}</div>
    </div>
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
