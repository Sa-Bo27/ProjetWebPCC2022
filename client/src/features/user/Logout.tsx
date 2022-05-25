import { useAuth0 } from "@auth0/auth0-react";
import React from "react";
import Button from 'react-bootstrap/Button'

const LogoutButton = () => {
  const { logout, isAuthenticated } = useAuth0();

  return (
    <>
    {isAuthenticated &&(
    <Button variant='danger' onClick={() => logout({ returnTo: window.location.origin })}>
      Log Out
    </Button>)}
    </>
  );
};

export default LogoutButton;
