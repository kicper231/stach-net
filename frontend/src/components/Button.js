import { useState } from "react";

export default function Button() {
  const [x, setX] = useState(null);

  function handleClick() {
    fetch("https://stachnet.azurewebsites.net/api/customers", { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setX(data);
      })
      .catch((error) => console.error(error));
  }

  return (
    <>
      {x && <p>{JSON.stringify(x)}</p>}
      <button onClick={handleClick}>Fetch</button>
    </>
  );
}
