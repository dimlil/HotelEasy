import React, { createContext, useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { decodeUserToken } from "../utils/auth.js";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const location = useLocation();

  const loadUserFromToken = async () => {
    const result = await decodeUserToken()

    setUser(result)
  };

  useEffect(() => {
    loadUserFromToken();
  }, [location]);

  return (
    <AuthContext.Provider value={{ user, setUser, loadUserFromToken }}>
      {children}
    </AuthContext.Provider>
  );
};