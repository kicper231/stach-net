import { useNavigate } from "react-router-dom";
import LandingPage from "./LandingPage";

export default function Menu() {
  const navigate = useNavigate();

  return (
    <>
      <LandingPage />
      <button onClick={() => navigate("/delivery-request/inquiry")}>
        Delivery request
      </button>
    </>
  );
}
