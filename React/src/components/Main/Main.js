import React, { useState, useEffect } from 'react';
import './Main.css';
import { api } from 'services';
import { WidgetTotal } from 'components/WidgetTotal';
import { faUsers, faFemale, faMale, faTimes, faCheck, faSuitcase, faMoneyBill } from '@fortawesome/free-solid-svg-icons'

// import { Container } from './styles';

export default function Main() {
  const [results, setResults] = useState({});
  const eventoId = 2;
  useEffect(() => {
    async function loadResults() {
      const response = await api.get(`/api/participantes`)
      setResults(response.data);
    }

    loadResults()
  }, [eventoId])

  return (
    <div className="main-container">
      <ul className="main-ul">
        <li>
          <WidgetTotal obj={
            {
              icon: faUsers,
              value: results.total,
              label: 'Participantes',
              className: 'primary'
            }
          } />
        </li>
        <li>
          <WidgetTotal obj={
            {
              icon: faCheck,
              value: results.confirmados,
              label: 'Confirmados',
              className: 'success'
            }
          } />
        </li>
        <li>
          <WidgetTotal obj={
            {
              icon: faSuitcase,
              value: results.presentes,
              label: 'Presentes',
              className: 'black'
            }
          } />
        </li>
        <li>
          <WidgetTotal obj={
            {
              icon: faTimes,
              value: results.cancelados,
              label: 'Cancelados',
              className: 'error'
            }
          } />
        </li>
      </ul>
      <ul className="second-ul">
        <li>
          <WidgetTotal obj={
            {
              icon: faMale,
              value: results.meninos,
              label: 'Meninos',
              className: 'boys'
            }
          } />
        </li>
        <li>
          <WidgetTotal obj={
            {
              icon: faFemale,
              value: results.meninas,
              label: 'Meninas',
              className: 'girls'
            }
          } />
        </li>
        <li>
          <WidgetTotal obj={
            {
              icon: faMoneyBill,
              value: results.totalPagar,
              label: 'Total Despesas',
              className: 'error'
            }
          } />
        </li>
        <li>
          <WidgetTotal obj={
            {
              icon: faMoneyBill,
              value: results.totalReceber,
              label: 'Total Receita',
              className: 'success'
            }
          } />
        </li>
      </ul>
    </div>
  );
}
