import "./App.css";
import "./components/button.css";
import NavBar from "./components/NavBar";
import Profile from "./components/Profile";
import Menu from "./components/Menu";

import { RequestInquiry } from "./components/RequestInquiry";
import { RequestOffers } from "./components/RequestOffers";
import { RequestSummary } from "./components/RequestSummary";
import { Test } from "./components/Test";

import { useAuth0 } from "@auth0/auth0-react";
import { Routes, Route } from "react-router-dom";

export default function App() {
  const { isAuthenticated } = useAuth0();

  function AccessDenied() {
    return <h1>Access denied</h1>;
  }

  return (
    <div className="background">
      <div className="navBarHolder">
        <NavBar />
      </div>
      <div className="contextHolder">
        <Routes>
          <Route path="/" element={<Menu />} />
          <Route path="/test" element={<Test />} />
          <Route
            path="/profile"
            element={isAuthenticated ? <Profile /> : AccessDenied()}
          />
          <Route
            path="/delivery_request/inquiry"
            element={<RequestInquiry />}
          />
          <Route path="/delivery_request/offers" element={<RequestOffers />} />
          <Route
            path="/delivery_request/summary"
            element={<RequestSummary />}
          />
        </Routes>
      </div>
    </div>
  );
}
