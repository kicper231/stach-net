const rolesUri = "https://stachnet.azurewebsites.net//roles";

export const AuthService = {
  isCourier: (user) => {
    return (
      user && Array.isArray(user[rolesUri]) && user[rolesUri].includes("Curier")
    );
  },

  isClient: (user) => {
    return (
      user && Array.isArray(user[rolesUri]) && user[rolesUri].includes("Client")
    );
  },

  isOfficeWorker: (user) => {
    return (
      user &&
      Array.isArray(user[rolesUri]) &&
      user[rolesUri].includes("OfficeWorker")
    );
  },
};
