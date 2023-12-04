import "./App.css";
import "./components/button.css";
import NavBar from "./components/NavBar";
import Profile from "./components/Profile";
import Menu from "./components/Menu";

import { useAuth0 } from "@auth0/auth0-react";
import { Routes, Route } from "react-router-dom";

export default function App() {
  const { isAuthenticated } = useAuth0();

  return (
    <div className="background">
      <div className="navBarHolder">
        <NavBar />
      </div>
      <div className="contextHolder">
        <Routes>
          <Route path="/" element={<Menu />} />
          <Route path="/profile" element={<Profile />} />
        </Routes>
      </div>
    </div>
  );
}
