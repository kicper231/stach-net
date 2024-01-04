import "./NavBar.css";
import LoginButton from "./LoginButton";
import LogoutButton from "./LogoutButton";
import { useAuth0 } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";

export default function NavBar() {
  const { isAuthenticated,user } = useAuth0();
  const navigate = useNavigate();

  const isCourier = user && Array.isArray(user["https://stachnet.azurewebsites.net//roles"]) && user["https://stachnet.azurewebsites.net//roles"].includes('Curier');
  const isClient = user && Array.isArray(user["https://stachnet.azurewebsites.net//roles"]) && user["https://stachnet.azurewebsites.net//roles"].includes('Client');
  console.log(isCourier);
  return (
    <div className="navBar">
      <div className="leftSide">
        <p className="name">MKS Courier</p>
      </div>
      <div className="rightSide">
        <button onClick={() => navigate("/")}>Home</button>
        {isAuthenticated && (
          <button onClick={() => navigate("/requests")}>Requests</button>
        )}
        {!isAuthenticated && <LoginButton />}
        {isAuthenticated && (
          <button onClick={() => navigate("/profile")}>Profile</button>
        )}
        {isAuthenticated && <LogoutButton />}


        {isAuthenticated && isCourier && (
          
          <button onClick={() => {/* kurierlogika */}}> KURIER</button>
        )}

        {isAuthenticated && isClient && (
          
          <button onClick={() => {/* kurierlogika */}}> Client</button>
        )}
        
      </div>
    </div>
  );
}
