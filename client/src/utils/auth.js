import { jwtDecode } from "jwt-decode";

export function decodeUserToken() {
  // const token = localStorage.getItem("userToken");
  // if (!token) return null;

  // try {
  //   const decoded = jwtDecode(token);
  //   return decoded;
  // } catch (error) {
  //   return null;
  // }

  const token = localStorage.getItem("userToken");
  if (!token) {
    return null;
  }

  try {
    const decoded = jwtDecode(token);
    return {
      role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
      id: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
    };
  } catch (err) {
    console.error("Invalid token:", err);
    localStorage.removeItem("token");
    return null;
  }
}
