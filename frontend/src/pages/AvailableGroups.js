import React, { useEffect, useState, useContext } from "react";
import axios from "../api/axiosInstance";
import { AuthContext } from "../context/AuthContext";
import "./AvailableGroups.css";

const AvailableGroups = () => {
  const { user } = useContext(AuthContext);
  const [groups, setGroups] = useState([]);
  const [children, setChildren] = useState([]);
  const [selectedChild, setSelectedChild] = useState("");
  const [success, setSuccess] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    // Dohvati sve dostupne grupe
    axios.get("/api/groups/available")
      .then(res => setGroups(res.data))
      .catch(() => setGroups([]));
    // Ako je roditelj, dohvati djecu
    if (user && user.role === "parent") {
      axios.get(`/api/children?parent_id=${user.id}`)
        .then(res => setChildren(res.data))
        .catch(() => setChildren([]));
    }
  }, [user]);

  const handleApply = async (groupId) => {
    setError("");
    setSuccess("");
    if (!selectedChild) {
      setError("Odaberite dijete za prijavu.");
      return;
    }
    try {
      await axios.post("/api/child_group", {
        child_id: selectedChild,
        group_id: groupId,
      });
      setSuccess("Uspješna prijava!");
    } catch (err) {
      setError("Greška pri prijavi. Provjerite podatke.");
    }
  };

  // Grupiranje po sportu (ako je potrebno)
  const groupsBySport = groups.reduce((acc, group) => {
    acc[group.sport_name] = acc[group.sport_name] || [];
    acc[group.sport_name].push(group);
    return acc;
  }, {});

  return (
    <div className="groups-container">
      <h2>Slobodne grupe</h2>
      {Object.keys(groupsBySport).map((sport) => (
        <div key={sport} className="group-sport-section">
          <h3>{sport}</h3>
          <div className="groups-list">
            {groupsBySport[sport].map((group) => (
              <div className="group-card" key={group.id}>
                <div><b>Grupa:</b> {group.name}</div>
                <div><b>Dob:</b> {group.min_age} - {group.max_age}</div>
                <div><b>Max članova:</b> {group.max_members}</div>
                {user && user.role === "parent" ? (
                  <>
                    <select
                      value={selectedChild}
                      onChange={e => setSelectedChild(e.target.value)}
                    >
                      <option value="">Odaberite dijete</option>
                      {children.map(child => (
                        <option key={child.id} value={child.id}>
                          {child.first_name} {child.last_name}
                        </option>
                      ))}
                    </select>
                    <button
                      onClick={() => handleApply(group.id)}
                      disabled={!selectedChild}
                    >
                      Prijavi dijete
                    </button>
                  </>
                ) : (
                  <button disabled title="Prijava dostupna samo roditeljima">
                    Prijavi dijete
                  </button>
                )}
              </div>
            ))}
          </div>
        </div>
      ))}
      {error && <div className="group-error">{error}</div>}
      {success && <div className="group-success">{success}</div>}
    </div>
  );
};

export default AvailableGroups;
