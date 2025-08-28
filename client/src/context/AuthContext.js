import React, { createContext, useState, useEffect } from "react";
import {jwtDecode} from "jwt-decode";
import { useLocation } from "react-router-dom";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const location = useLocation();

  const loadUserFromToken = () => {
    const token = localStorage.getItem("userToken");
    if (!token) {
      setUser(null);
      return;
    }

    try {
      const decoded = jwtDecode(token);
      setUser({
        role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
        email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        id: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
      });
    } catch (err) {
      console.error("Invalid token:", err);
      localStorage.removeItem("token");
      setUser(null);
    }
  };

  // ще се извиква при всяка смяна на route
  useEffect(() => {
    loadUserFromToken();
  }, [location]);

  return (
    <AuthContext.Provider value={{ user, setUser, loadUserFromToken }}>
      {children}
    </AuthContext.Provider>
  );
};
