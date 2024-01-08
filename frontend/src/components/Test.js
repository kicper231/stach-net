import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import React, { useState, useEffect } from "react";

const domain = process.env.REACT_APP_AUTH0_DOMAIN;

const data = {
  id: 1,
  firstName: "Adam",
  lastName: "Nowak",
};

export function Test() {
  const [activeUsers, setActiveUsers] = useState(null);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const callApi = async () => {
      try {
        const token = await getAccessTokenSilently();
        const response = await fetch(
          `https://${domain}/api/v2/stats/active-users/`,
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        const responseData = await response.json();

        setActiveUsers(responseData);
        //   setState({
        //     ...state,
        //     showResult: true,
        //     apiMessage: responseData,
        //   });
      } catch (error) {
        setActiveUsers("3");
      }
    };

    callApi();
  });

  function handleGet() {
    fetch("http://localhost:5157/test_get", { method: "GET" })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
      })
      .catch((error) => console.error(error));
  }

  const handlePostFetch = async () => {
    try {
      const response = await fetch("http://localhost:5157/test_post_fetch", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {
        console.log("Form submitted successfully!");
      } else {
        console.error("Form submission failed:", response.statusText);
      }
    } catch (error) {
      console.error("Error sending form data:", error);
    }
  };

  const handlePostAxios = async () => {
    try {
      const response = await axios.post(
        "http://localhost:5157/test_post_axios",
        data
      );
      console.log("Data sent successfully:", response.data);
    } catch (error) {
      console.error("Error sending data to the server:", error);
    }
  };

  return (
    <div>
      <h1>TEST</h1>
      {activeUsers ? (
        <p>Number of Active Users: {activeUsers}</p>
      ) : (
        <p>Loading...</p>
      )}
      <button onClick={handleGet}>Get data</button>
      <button onClick={handlePostFetch}>Post with Fetch</button>
      <button onClick={handlePostAxios}>Post with Axios</button>
    </div>
  );
}
