import { useEffect, useState } from "react";

//const serverUrl = process.env.SERVER_URL;

export default function LandingPage() {
  // const [number, setNumber] = useState(-1);
  const [number, setNumber] = useState({ numberOfLogins: -1 }); // zmiienione

  useEffect(() => {
    // fetch(`${serverUrl}/users/ActiveUsers`, { method: "GET" })
    fetch(`https://localhost:7161/api/users/ActiveUsers`, { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        setNumber(data);
      })
      .catch((error) => console.error(error));
  }, []); // bo inaczej zapetla sie w kołko (?) to  poprawić bo widac -1 przez chwile zanim dojdzie fetch i ogólnie jakis system tego renderownaia idk

  return (
    <p>
      MKS Courier is an average, unremarkable company for transporting your
      packages. <br /> Please note that we do not guarantee that your package
      will arrive in one piece. <br /> Number of users: {number.numberOfLogins}.
    </p>
  );
}
