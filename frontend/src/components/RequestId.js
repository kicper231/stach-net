import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";

export function RequestId() {
  const location = useLocation();
  const [id, setId] = useState(-1);

  useEffect(() => {
    setId(location.state.requestId.offerRequestId);
  }, [location.state.requestId.offerRequestId]);

  return <h1>Your request id: {id}</h1>;
}
