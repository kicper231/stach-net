import "./App.css";
import "./components/button.css";
import NavBar from "./components/NavBar";
import Profile from "./components/Profile";
import Menu from "./components/Menu";
import { Inquiries } from "./components/Inquiries";

import { RequestInquiry } from "./components/RequestInquiry";
import { RequestOffers } from "./components/RequestOffers";
import { RequestSummary } from "./components/RequestSummary";
import { RequestId } from "./components/RequestId";

import { useAuth0 } from "@auth0/auth0-react";
import { Routes, Route } from "react-router-dom";
import { AuthService } from "./components/AuthService";
import { WorkerOffers } from "./components/WorkerOffers";
import { WorkerInquiries } from "./components/WorkerInquiries";
import { CourierDeliveries } from "./components/CourierDeliveries";
import { CourierMyDeliveries } from "./components/CourierMyDeliveries";

export default function App() {
  const { isLoading, isAuthenticated, user } = useAuth0();

  function AccessDenied() {
    return <h1>Access denied</h1>;
  }

  return (
    <div className="background">
      <div className="navBarHolder">
        <NavBar />
      </div>
      <div className="contextHolder">
        {isLoading ? (
          <h1>Loading . . .</h1>
        ) : (
          <Routes>
            <Route path="/" element={<Menu />} />
            <Route
              path="/profile"
              element={isAuthenticated ? <Profile /> : AccessDenied()}
            />
            <Route
              path="/requests/*"
              element={isAuthenticated ? <Inquiries /> : AccessDenied()}
            />
            <Route
              path="/deliveries/*"
              element={
                isAuthenticated && AuthService.isCourier(user) ? (
                  <CourierDeliveries />
                ) : (
                  AccessDenied()
                )
              }
            />
            <Route
              path="/my-deliveries/*"
              element={
                isAuthenticated && AuthService.isCourier(user) ? (
                  <CourierMyDeliveries />
                ) : (
                  AccessDenied()
                )
              }
            />
            <Route
              path="/inquiries/*"
              element={
                isAuthenticated && AuthService.isOfficeWorker(user) ? (
                  <WorkerInquiries />
                ) : (
                  AccessDenied()
                )
              }
            />
            <Route
              path="/offers/*"
              element={
                isAuthenticated && AuthService.isOfficeWorker(user) ? (
                  <WorkerOffers />
                ) : (
                  AccessDenied()
                )
              }
            />
            <Route
              path="/delivery-request/inquiry"
              element={<RequestInquiry />}
            />
            <Route
              path="/delivery-request/offers"
              element={<RequestOffers />}
            />
            <Route
              path="/delivery-request/summary"
              element={<RequestSummary />}
            />
            <Route path="/delivery-request/id" element={<RequestId />} />
          </Routes>
        )}
      </div>
    </div>
  );
}
