import axios from "axios";
import { useEffect, useState } from "react";
import { config } from "../config-development";

export default function LandingPage() {
  const [data, setData] = useState();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          `${config.serverUri}/users/activeusers`
        );
        setData(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchData();
  }, []);

  if (!data) return <p>Loading ...</p>;

  return (
    <p>
      MKS Courier is an average, unremarkable company for transporting your
      packages. <br /> Please note that we do not guarantee that your package
      will arrive in one piece. <br /> Number of logins on our website:{" "}
      {data.numberOfLogins}
    </p>
  );
}
