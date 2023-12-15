import { useState } from "react";

const dataForPost = {
  id: 0,
  createdAt: "2023-12-14T20:16:58.384Z",
  firstName: "string",
  lastName: "string",
  gender: 0,
};

export default function Button() {
  const [dataFromGet, setDataFromGet] = useState(null);

  function handleClickGet() {
    fetch("http://localhost:5157/api/customers", { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        setDataFromGet(data);
      })
      .catch((error) => console.error(error));
  }

  function handleClickPost() {
    try {
      const response = fetch("http://localhost:5157/api/customers", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(dataForPost),
      });

      if (response.ok) {
        console.log("Form submitted successfully!");
      } else {
        console.error("Form submission failed:", response.statusText);
      }
    } catch (error) {
      console.error("Error sending form data:", error);
    }
  }

  return (
    <>
      {dataFromGet && <p>{JSON.stringify(dataFromGet)}</p>}
      <button onClick={handleClickGet}>Get</button>

      {dataForPost && <p>{JSON.stringify(dataForPost)}</p>}
      <button onClick={handleClickPost}>Post</button>
    </>
  );
}
