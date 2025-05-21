import React from "react";
import "./HomePage.css";

const sports = [
  { name: "Nogomet", icon: "⚽️" },
  { name: "Tenis", icon: "🎾" },
  { name: "Plivanje", icon: "🏊‍♂️" },
];

const HomePage = () => {
  return (
    <div className="homepage">
      <div className="homepage-animation">
        {/* Jednostavna animacija za djecu i roditelje */}
        <div className="ball"></div>
        <h2>Dobrodošli u sportsku školicu!</h2>
        <p>
          Prijavite svoje dijete na sportsku aktivnost ili se pridružite kao trener.
        </p>
      </div>
      <div className="homepage-sports">
        <h3>Sportovi koje nudimo:</h3>
        <div className="sports-list">
          {sports.map((sport) => (
            <div className="sport-card" key={sport.name}>
              <span className="sport-icon">{sport.icon}</span>
              <span>{sport.name}</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default HomePage;
