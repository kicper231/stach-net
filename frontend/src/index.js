import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";

import { Auth0Provider } from "@auth0/auth0-react";
import { BrowserRouter } from "react-router-dom";

const domain = process.env.REACT_APP_AUTH0_DOMAIN;
const clientId = process.env.REACT_APP_AUTH0_CLIENT_ID;
const audience = process.env.REACT_APP_AUDIENCE;

const root = ReactDOM.createRoot(document.getElementById("root"));

const providerConfig = {
  domain: domain,
  clientId: clientId,
  redirectUri: window.location.origin,
  ...(audience ? { audience: audience } : null),

};


console.log(audience);


root.render(
  <React.StrictMode>
    <Auth0Provider
      {...providerConfig}


      
    >
      <BrowserRouter>
        <App />
     </BrowserRouter>
    </Auth0Provider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
