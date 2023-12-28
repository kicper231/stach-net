import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import React, { useState, useEffect } from "react";

const domain = process.env.REACT_APP_AUTH0_DOMAIN;

export function Test() {
  const [activeUsers, setActiveUsers] = useState(null);
  const [data, setData] = useState({});

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
        setActiveUsers("ERROR");
      }
    };

    callApi();
  });

  useEffect(() => {
    const sendDataWithFetch = async () => {
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

    const sendDataWithAxios = async () => {
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

    setData({ id: 1, string: "abc" });
    sendDataWithFetch();
    sendDataWithAxios();
  }, [data]);

  return (
    <div>
      <h1>TEST</h1>
      {activeUsers ? (
        <p>Number of Active Users: {activeUsers}</p>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}
