import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";

import { Auth0Provider } from "@auth0/auth0-react";
import { BrowserRouter } from "react-router-dom";
import { config } from "./config-development";

const root = ReactDOM.createRoot(document.getElementById("root"));

const providerConfig = {
  domain: config.auth0domain,
  clientId: config.auth0clientId,
  authorizationParams: {
    redirect_uri: window.location.origin,
    ...(config.audience ? { audience: config.audience } : null),
  },
};

root.render(
  // <React.StrictMode>
  <Auth0Provider {...providerConfig}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Auth0Provider>
  // </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
