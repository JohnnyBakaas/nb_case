import styles from "./Header.module.css";

type HeaderProps = {
  setLightMode: (value: any) => void;
};

const Header = ({ setLightMode }: HeaderProps) => {
  return (
    <header className={styles.header}>
      <div>
        <h1>ProffSentralen</h1>
        <button
          onClick={() => {
            setLightMode((e: any) => !e); // Endre dette så det ikke er any
          }}
        >
          Light/Dark mode
        </button>
      </div>
    </header>
  );
};

export default Header;
