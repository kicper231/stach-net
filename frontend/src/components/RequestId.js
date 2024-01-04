import { useEffect, useState } from "react";

const serverUrl = process.env.SERVER_URL;

export function RequestId() {
  const [id, setId] = useState(-1);

  useEffect(() => {
    fetch(`${serverUrl}/id`, { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        setId(data);
      })
      .catch((error) => console.error(error));
  });

  return <h1>Your request id: {id}</h1>;
}
