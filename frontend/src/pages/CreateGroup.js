import React, { useState, useEffect, useContext } from "react";
import axios from "../api/axiosInstance";
import { AuthContext } from "../context/AuthContext";
import "./CreateGroup.css";

const CreateGroup = () => {
  const { user } = useContext(AuthContext);
  const [sports, setSports] = useState([]);
  const [form, setForm] = useState({
    name: "",
    sport_id: "",
    max_members: "",
    min_age: "",
    max_age: "",
  });
  const [success, setSuccess] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    // Dohvati sve sportove (npr. /api/sports)
    axios.get("/api/sport")
      .then(res => {
        console.log("fetched sports: " + res)
        setSports(res.data)
      })
      .catch(() => setSports([]));
  }, []);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    if (!form.name || !form.sport_id || !form.max_members || !form.min_age || !form.max_age) {
      setError("Popunite sva polja.");
      return;
    }
    if (parseInt(form.min_age) > parseInt(form.max_age)) {
      setError("Minimalna dob ne može biti veća od maksimalne.");
      return;
    }
    try {
      await axios.post("/api/sportgroup", {
        trainerid: user.id,
        name: form.name,
        sportid: form.sport_id,
        maxmembers: form.max_members,
        minage: form.min_age,
        maxage: form.max_age,
      });
      setSuccess("Grupa je uspješno kreirana!");
      setForm({
        name: "",
        sport_id: "",
        max_members: "",
        min_age: "",
        max_age: "",
      });
    } catch (err) {
      setError("Greška pri kreiranju grupe. Provjerite podatke.");
    }
  };

  if (!user || user.role !== "trainer") {
    return <div className="creategroup-info">Samo treneri mogu kreirati grupu.</div>;
  }

  return (
    <div className="creategroup-container">
      <h2>Kreiraj grupu</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="name"
          placeholder="Naziv grupe"
          value={form.name}
          onChange={handleChange}
          required
        />
        <select
          name="sport_id"
          value={form.sport_id}
          onChange={handleChange}
          required
        >
          <option value="">Odaberite sport</option>
          {sports.map(sport => (
            <option key={sport.id} value={sport.id}>
              {sport.name}
            </option>
          ))}
        </select>
        <input
          type="number"
          name="max_members"
          placeholder="Max broj članova"
          min="1"
          value={form.max_members}
          onChange={handleChange}
          required
        />
        <input
          type="number"
          name="min_age"
          placeholder="Minimalna dob"
          min="1"
          value={form.min_age}
          onChange={handleChange}
          required
        />
        <input
          type="number"
          name="max_age"
          placeholder="Maksimalna dob"
          min="1"
          value={form.max_age}
          onChange={handleChange}
          required
        />
        <button type="submit">Kreiraj grupu</button>
        {error && <div className="creategroup-error">{error}</div>}
        {success && <div className="creategroup-success">{success}</div>}
      </form>
    </div>
  );
};

export default CreateGroup;
