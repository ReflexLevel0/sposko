import React, { useContext, useEffect, useState } from "react";
import axios from "../api/axiosInstance";
import { AuthContext } from "../context/AuthContext";
import "./Profile.css";

const Profile = () => {
  const { user } = useContext(AuthContext);
  const [profile, setProfile] = useState(null);
  const [children, setChildren] = useState([]);
  const [showAddChild, setShowAddChild] = useState(false);
  const [childForm, setChildForm] = useState({
    first_name: "",
    last_name: "",
    date_of_birth: "",
    phone_number: "",
    email: "",
  });
  const [success, setSuccess] = useState("");
  const [error, setError] = useState("");

  // Dohvati podatke o korisniku
  useEffect(() => {
    if (user) {
      axios.get("/api/users/me")
        .then(res => setProfile(res.data))
        .catch(() => setProfile(null));
      if (user.role === "parent") {
        axios.get(`/api/children?parent_id=${user.id}`)
          .then(res => setChildren(res.data))
          .catch(() => setChildren([]));
      }
    }
  }, [user]);

  const handleChildChange = (e) => {
    setChildForm({ ...childForm, [e.target.name]: e.target.value });
  };

  const handleAddChild = async (e) => {
    e.preventDefault();
    setError("");
    setSuccess("");
    if (
      !childForm.first_name ||
      !childForm.last_name ||
      !childForm.date_of_birth ||
      !childForm.phone_number ||
      !childForm.email
    ) {
      setError("Popunite sva polja za dijete.");
      return;
    }
    try {
      await axios.post("/api/children", {
        parent_id: user.id,
        ...childForm,
      });
      setSuccess("Dijete uspješno dodano!");
      setShowAddChild(false);
      setChildForm({
        first_name: "",
        last_name: "",
        date_of_birth: "",
        phone_number: "",
        email: "",
      });
      // Osvježi listu djece
      const res = await axios.get(`/api/children?parent_id=${user.id}`);
      setChildren(res.data);
    } catch (err) {
      setError("Greška pri dodavanju djeteta.");
    }
  };

  if (!user) return <div className="profile-info">Prijavite se za pristup profilu.</div>;
  if (!profile) return <div className="profile-info">Učitavanje profila...</div>;

  return (
    <div className="profile-container">
      <h2>Moj profil</h2>
      <div className="profile-card">
        <div><b>Ime:</b> {profile.first_name}</div>
        <div><b>Prezime:</b> {profile.last_name}</div>
        <div><b>Email:</b> {profile.email}</div>
        <div><b>Telefon:</b> {profile.phone_number}</div>
        {user.role === "trainer" && (
          <>
            <div><b>Datum rođenja:</b> {profile.date_of_birth}</div>
            <div><b>O treneru:</b> {profile.info}</div>
            <div><b>Status:</b> {profile.verified ? "Odobren" : "Na čekanju"}</div>
          </>
        )}
      </div>

      {user.role === "parent" && (
        <div className="profile-children-section">
          <h3>Moja djeca</h3>
          {children.length === 0 ? (
            <div>Nema dodane djece.</div>
          ) : (
            <ul>
              {children.map(child => (
                <li key={child.id}>
                  {child.first_name} {child.last_name} ({child.date_of_birth})
                </li>
              ))}
            </ul>
          )}
          {showAddChild ? (
            <form className="add-child-form" onSubmit={handleAddChild}>
              <input
                type="text"
                name="first_name"
                placeholder="Ime"
                value={childForm.first_name}
                onChange={handleChildChange}
                required
              />
              <input
                type="text"
                name="last_name"
                placeholder="Prezime"
                value={childForm.last_name}
                onChange={handleChildChange}
                required
              />
              <input
                type="date"
                name="date_of_birth"
                placeholder="Datum rođenja"
                value={childForm.date_of_birth}
                onChange={handleChildChange}
                required
              />
              <input
                type="text"
                name="phone_number"
                placeholder="Telefon"
                value={childForm.phone_number}
                onChange={handleChildChange}
                required
              />
              <input
                type="email"
                name="email"
                placeholder="Email"
                value={childForm.email}
                onChange={handleChildChange}
                required
              />
              <button type="submit">Dodaj dijete</button>
              <button type="button" onClick={() => setShowAddChild(false)}>
                Odustani
              </button>
            </form>
          ) : (
            <button className="add-child-btn" onClick={() => setShowAddChild(true)}>
              Dodaj dijete
            </button>
          )}
        </div>
      )}
      {error && <div className="profile-error">{error}</div>}
      {success && <div className="profile-success">{success}</div>}
    </div>
  );
};

export default Profile;
