import "./NavBar.css";
import LoginButton from "./LoginButton";
import LogoutButton from "./LogoutButton";
import { useAuth0 } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";

export default function NavBar() {
  const { isAuthenticated } = useAuth0();
  const navigate = useNavigate();

  return (
    <div className="navBar">
      <div className="leftSide">
        <p className="name">Mój Szybki Kurier</p>
      </div>
      <div className="rightSide">
        <button onClick={() => navigate("/")}>Home</button>
        {!isAuthenticated && <LoginButton />}
        {isAuthenticated && (
          <button onClick={() => navigate("/profile")}>Profil</button>
        )}
        {isAuthenticated && <LogoutButton />}
      </div>
    </div>
  );
}
