import { useState } from "react";
import { useNavigate } from "react-router-dom";

const serverUrl = process.env.SERVER_URL;

export function RequestInquiry() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    userAuth0: '12345abcdef',
    package: {
        width: 15,
        height: 10,
        length: 20,
        weight: 5.5,
    },
    sourceAddress: {
        houseNumber: '12A',
        apartmentNumber: '3',
        street: 'Kwiatowa',
        city: 'Kraków',
        postalCode: '30-001',
        country: 'Polska',
    },
    destinationAddress: {
        houseNumber: '55B',
        apartmentNumber: '7',
        street: 'Wolności',
        city: 'Warszawa',
        postalCode: '00-950',
        country: 'Polska',
    },
    deliveryDate: '2024-03-15',
    priority: true,
    weekendDelivery: true,
});


  const handleChange = (e, key = null) => {
    var value = e.target.value;

    if (e.target.name === "weekendDelivery") {
      value = e.target.checked;
    }

    if (e.target.name === "priority") {
      value = value === "option1" ? true : false;
    }
    if (e.target.name === "vipPackage") {
      value = value === "option1" ? true : false;
    }
    if (e.target.name === "IsCompany") {
      value = value === "option3" ? true : false;
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
   //e.preventDefault();
  //  console.log(JSON.stringify(formData));
  navigate("/delivery-request/offers", { state: { formData: formData } });

    
  };

  return (
    <div className="overflow">
      <form onSubmit={handleSubmit}>
        <p>Package</p>

        <label>
          width:
          <input
            type="text"
            name="width"
            value={formData.package.width}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>
        <br />
        <label>
        height:
          <input
            type="text"
            name="dimensions"
            value={formData.package.height}
            onChange={(e) => handleChange(e, "package")}
          />
        </label>
        <br />
        
        <label>
        length:
          <input
            type="text"
            name="length"
            value={formData.package.length}
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
        houseNumber:
          <input
            type="text"
            name="houseNumber"
            value={formData.sourceAddress.houseNumber}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
        <label>
        apartmentNumber:
          <input
            type="text"
            name="apartmentNumber"
            value={formData.sourceAddress.apartmentNumber}
            onChange={(e) => handleChange(e, "sourceAddress")}
          />
        </label>
        <br />
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
        houseNumber:
          <input
            type="text"
            name="houseNumber"
            value={formData.destinationAddress.houseNumber}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
        <label>
        apartmentNumber:
          <input
            type="text"
            name="apartmentNumber"
            value={formData.destinationAddress.apartmentNumber}
            onChange={(e) => handleChange(e, "destinationAddress")}
          />
        </label>
        <br />
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
            

      
       
       

        <br/>
        <button type="submit">Send delivery request</button>
      </form>
    </div>
  );
}
