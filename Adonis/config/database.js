'use strict'

/** @type {import('@adonisjs/framework/src/Env')} */
const Env = use('Env')

/** @type {import('@adonisjs/ignitor/src/Helpers')} */
const Helpers = use('Helpers')

module.exports = {

  connection: Env.get('DB_CONNECTION', 'mssql'),

  mssql: {
    client: 'mssql',
    connection: {
      host: 'localhost',
      user: 'sa',
      password: '123456',
      database: 'SysIgreja'
    }
  }

}
