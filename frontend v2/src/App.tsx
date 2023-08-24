import { useState } from "react";
import styles from "./App.module.css";
import Header from "./components/header/Header";
import Main from "./components/main/Main";

function App() {
  const [lightMode, setLightMode] = useState(false);

  return (
    <div
      className={` ${styles.wrapper} ${
        lightMode ? styles.lightMode : styles.darkMode
      }`}
    >
      <Header setLightMode={setLightMode} />
      <Main />
    </div>
  );
}

export default App;
