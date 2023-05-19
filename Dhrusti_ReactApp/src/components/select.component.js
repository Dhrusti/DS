const Select = (props) => {
    return (
        <select className="form-select" aria-label={props.selectTitle} onChange={props.setOnDrpChange}>
            <option defaultValue>{props.selectTitle}</option>
            {props.data.map((element) => <option key={element.id} value={element.id}>{element.name}</option>)}
        </select>
    )
}

export default Select;