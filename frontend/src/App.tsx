import { useState } from "react";
import Form from "./components/form/Form";
import styles from "./App.module.css";

function App() {
  const [darkMode, setDarkMode] = useState<boolean>(true);

  return (
    <div
      className={darkMode ? styles.darkMode : styles.lightMode}
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Form />
      <button
        onClick={() => {
          setDarkMode((e) => !e);
        }}
      >
        Endre farge
      </button>
    </div>
  );
}

export default App;
