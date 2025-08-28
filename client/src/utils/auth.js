import {jwtDecode} from "jwt-decode";

export function getUserRole() {
  const token = localStorage.getItem("userToken");
  if (!token) return null;

  try {
    const decoded = jwtDecode(token);
    
    return decoded.role;
  } catch (error) {
    return null;
  }
}
