import { useAuth0 } from "@auth0/auth0-react";
import React, { useState, useEffect } from "react";

export function Test() {
  const [activeUsers, setActiveUsers] = useState(null);

  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const callApi = async () => {
      try {
        const token = await getAccessTokenSilently();
        const domain = "dev-6i665fwgjuvholkh.us.auth0.com";
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

  return (
    <div>
      <h2>Auth0 Active Users</h2>
      {activeUsers ? (
        <p>Number of Active Users: {activeUsers}</p>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}