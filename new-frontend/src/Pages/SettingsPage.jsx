import CardList from "../Components/CardList";

import './SettingsPage.jsx.css'

import settingsPages from '../Util/settingsPages';

function SettingsPage() {
    return (
        <main className="settings-page w100">
            {settingsPages.map((page) => {
                return <CardList title={page.title} items = {page.items} key={page.title} />
            })}
        {/* <CardList title={"Tapper"} items = {["Barboekblad invullen", "Notitie achterlaten", "Mutatie kasla", "Kasla actief maken", "Extern verbruik invoeren", "View mode aanpassen"]} />
        <CardList title={"Account informatie"} items = {["Mijn informatie", "Door mij ingevoerde informatie", "Informatie van een ander account", "Informatie van alle accounts"]} />
        <CardList title={"Korting"} items = {["Korting voorbereiden", "Korting starten in deze sessie", "Kortingen-overzicht"]} />
        <CardList title={"Pasjes"} items = {["Koppelen", "Ontkoppelen"]} />
        <CardList title={"Account informatie"} items = {["Mijn informatie", "Door mij ingevoerde informatie", "Informatie van een ander account", "Informatie van alle accounts"]} /> */}

        </main>
    )
}

export default SettingsPage;