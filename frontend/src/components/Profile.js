import { useAuth0 } from "@auth0/auth0-react";


export default function Profile() {
  const { user, isAuthenticated } = useAuth0();
  return (
    isAuthenticated && (
      <>
        {user?.picture && <img src={user.picture} alt={user?.name} />}
        <ul>  
          {Object.keys(user).map((objKey, i) => (
            <li key={i}>
              {objKey}: {user[objKey]} 
            </li>
          ))}
         
        </ul>
      </>
    )
  );
}

