import { useEffect, useState } from "react";

const serverUrl = process.env.SERVER_URL;

export default function LandingPage() {
  const [number, setNumber] = useState(-1);

  useEffect(() => {
    fetch(`${serverUrl}/total-users`, { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        setNumber(data);
      })
      .catch((error) => console.error(error));
  });

  return (
    <p>
      MKS Courier is an average, unremarkable company for transporting your
      packages. <br /> Please note that we do not guarantee that your package
      will arrive in one piece. <br /> Number of users: {number}.
    </p>
  );
}
