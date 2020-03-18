export default class Storage {
  constructor (namespace) {
    this.namespace = namespace ? `${namespace}.` : '';
  }

  get (name, defaultValue) {
    const value = localStorage.getItem(this.namespace + name);
    return value === null ? defaultValue : JSON.parse(value);
  }

  set (name, value) {
    localStorage.setItem(this.namespace + name, JSON.stringify(value));
  }
}
