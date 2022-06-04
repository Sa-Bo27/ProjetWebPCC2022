import React from "react";
import ReactDOM from "react-dom/client";
import "./app/layout/style.css";
import App from "./app/layout/App";
import reportWebVitals from "./reportWebVitals";
import { Auth0Provider } from "@auth0/auth0-react";


const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <Auth0Provider 
      domain="dev-gi8iib45.us.auth0.com" 
      clientId="72VO5odflD2bdHYjIpkS3jpzB43zrfnJ"
      redirectUri={window.location.origin}
      audience="https://localhost:7280"
      scope="read:user write:movie"
      >
        
      <App />
    </Auth0Provider>
  </React.StrictMode>
);

reportWebVitals();
