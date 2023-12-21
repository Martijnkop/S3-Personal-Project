import { BrowserRouter, Route, Routes } from "react-router-dom"

import Navbar from "../../Views/Navbar"

import HomePage from "../../Pages/HomePage"
import SettingsPage from "../../Pages/SettingsPage"
import CreateAccount from "../../Pages/Settings/CreateAccount"
import ProductsPage from "../../Pages/ProductsPage"
import CreateItem from "../../Pages/Settings/CreateItem"
import CreateOrEditPriceType from "../../Pages/Settings/Finances/CreateOrEditPriceType"
import CreateTaxType from "../../Pages/Settings/Finances/CreateTaxType"
import ManagePriceTypes from "../../Pages/Settings/Finances/ManagePriceTypes"
import ManageTaxes from "../../Pages/Settings/Finances/ManageTaxes"
import CreateOrEditTaxTypeInstance from "../../Pages/Settings/Finances/CreateOrEditTaxInstance"
import ManageItemCategories from "../../Pages/Settings/ManageItemCategories"
import CreateOrEditItemCategory from "../../Pages/Settings/CreateOrEditItemCategory"
import FinancialOverviewPage from "../../Pages/FiancialOverviewPage"

function Router() {
    return (
        <BrowserRouter>
            <Navbar />

            <Routes>
                <Route path="/" element = {<HomePage />}/>
                <Route path="/products" element = {<ProductsPage />}/>
                <Route path="/settings" element = {<SettingsPage />} />
                    <Route path="/create-account" element = {<CreateAccount />} />
                    <Route path="/create-item" element = {<CreateItem />} />

                    <Route path="/edit-pricetype" element = {<CreateOrEditPriceType />} />
                    <Route path="/manage-pricetypes" element = {<ManagePriceTypes />} />

                    <Route path="/edit-tax" element = {<CreateTaxType />} />
                    <Route path="/manage-taxes" element = {<ManageTaxes />} />
                    <Route path="/edit-tax-instance" element = {<CreateOrEditTaxTypeInstance />} />

                    <Route path="/manage-item-categories" element = {<ManageItemCategories />} />
                    <Route path="/edit-itemcategory" element = {<CreateOrEditItemCategory />} />

                    <Route path="/finances" element = {<FinancialOverviewPage />} />
            </Routes>
        </BrowserRouter>
    )
}

export default Router