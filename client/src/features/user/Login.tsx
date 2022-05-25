import { useAuth0 } from "@auth0/auth0-react";
import React from "react";
import Button from 'react-bootstrap/Button'
const LoginButton = () => {
  const { loginWithRedirect, isAuthenticated } = useAuth0();

  return (
  <>
    {!isAuthenticated && 
  <Button variant="secondary" onClick={() => loginWithRedirect()}>Log In</Button>};
  </>
  )
  
  
};

export default LoginButton;